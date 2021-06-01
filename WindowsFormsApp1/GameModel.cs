using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    enum CellState
    {
        O = -1,
        Empty,
        X
    }

    enum GameResult
    {
        IsGoing,
        Draw,
        Won
    }

    class GameModel
    {
        private CellState[] Field;
        private CellState CurrentTurn;
        private bool AgainstAI;
        private Socket Socket;
        public string IP = "127.0.0.1";
        public int Port = 8080;
        public int side = 4;

        public GameModel(CellState firstTurn, bool againstAI)
        {
            CurrentTurn = firstTurn;
            Field = new CellState[side*side];
            AgainstAI = againstAI;
        }

        public async void MakeTurn(int position)
        {
            GameResult result = ChangeField(position);

            if (AgainstAI)
            {
                if (Socket == null)
                {
                    IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
                    Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    BeforeSendAction?.Invoke();

                    try
                    {
                        await Task.Run(() => Socket.Connect(ipPoint));
                    }
                    catch (Exception ex)
                    {
                        ErrorOccured?.Invoke(ex.Message);
                        return;
                    }
                    AfterSendAction?.Invoke();
                    Socket.SendTimeout = 10000;
                    Socket.ReceiveTimeout = 10000;
                }

                BeforeSendAction?.Invoke();

                try
                {
                    await Task.Run(() => Socket.Send(new byte[] { (byte)position }));
                }
                catch (SocketException ex)
                {
                    ErrorOccured?.Invoke(ex.Message);
                    return;
                }

                if (result == GameResult.IsGoing)
                {
                    byte[] data = new byte[256];

                    try
                    {
                        await Task.Run(() => { int bytes = Socket.Receive(data, data.Length, 0); });
                    }
                    catch (SocketException ex)
                    {
                        ErrorOccured?.Invoke(ex.Message);
                        return;
                    }
                    AfterSendAction?.Invoke();
                    ChangeField(data[0]);
                }
            }
        }

        public void CloseConnection()
        {
            if (Socket != null)
                Socket.Close();
        }

        private GameResult ChangeField(int position)
        {
            Field[position] = CurrentTurn;
            MadeTurn?.Invoke(position, CurrentTurn);
            CurrentTurn = CurrentTurn == CellState.O ? CellState.X : CellState.O;

            (GameResult result, int[] wonPos) = CheckIfGameEnd();
            if (result != GameResult.IsGoing)
            {
                CurrentTurn = CellState.X;
                for (int i = 0; i < side*side; i++)
                    Field[i] = CellState.Empty;
                GameEnd?.Invoke(result, wonPos);

            }
            return result;
        }

        private (GameResult result, int[] wonPos) CheckIfGameEnd()
        {
            bool a = true;
            CellState sign;
            List<int> wonPos = new List<int>();
            for (int i = 0; i < side; i++)//по вертикали
            {
                wonPos.Clear();
                sign = Field[i];
                if (sign == CellState.Empty)
                    continue;
                for (var j = 1; j < side; j++)
                {
                    wonPos.Add(i + j * side);
                    if (Field[i+j*side] != sign)
                    {
                        a = false;
                        break;
                    }
                }
                if (a == true)
                    return (GameResult.Won, wonPos.ToArray());
            }
            a = true;
            for (int i = 0; i < side; i ++)//по горизонтали
            {
                wonPos.Clear();
                sign = Field[i];
                if (sign == CellState.Empty)
                    continue;
                for (var j = 1; j < side; j++)
                {
                    wonPos.Add(i * side + j);
                    if (Field[i*side + j] != sign)
                    {
                        a = false;
                        break;
                    }
                }
                if (a == true)
                    return (GameResult.Won, wonPos.ToArray());
            }
            a = true;
            sign = Field[0];
            wonPos.Clear();
            if (sign != CellState.Empty)
            {
                for (int i = 0; i < side; i++)
                {
                    wonPos.Add(i + side * i);
                    if (Field[i + side * i] != sign)
                    {
                        a = false;
                        break;
                    }
                }
                if (a == true)
                    return (GameResult.Won, wonPos.ToArray());
            }

            wonPos.Clear();
            if (sign != CellState.Empty)
            {
                for (int i = side - 1; i < side * side; i += side - 1)
                {
                    wonPos.Add(i + side * i);
                    if (Field[i + side * i] != sign)
                    {
                        a = false;
                        break;
                    }
                }
                if (a == true)
                    return (GameResult.Won, wonPos.ToArray());
            }


            foreach (var item in Field)
            {
                if (item == CellState.Empty)
                    return (GameResult.IsGoing, null);
            }

            return (GameResult.Draw, null);
        }

        public event Action<int, CellState> MadeTurn;
        public event Action<GameResult, int[]> GameEnd;
        public event Action BeforeSendAction;
        public event Action AfterSendAction;
        public event Action<string> ErrorOccured;
    }
}
