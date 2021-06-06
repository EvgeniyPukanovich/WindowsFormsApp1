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

        private bool AIFirst;
        public int Side { get; private set; }

        public GameModel(bool againstAI, bool AIFirst, int side)
        {
            Side = side;
            CurrentTurn = CellState.X;
            Field = new CellState[side * side];
            this.AIFirst = AIFirst;
            AgainstAI = againstAI;
        }

        public async void FirstConnect()
        {
            if (!AgainstAI)
                return;
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            BeforeSendAction?.Invoke();
            try
            {
                await Task.Run(() =>
                {
                    Socket.Connect(ipPoint);
                    if (AIFirst)
                    {
                        Socket.Send(new byte[] { (byte)Side, 0 });
                        byte[] data = new byte[256];
                        int bytes = Socket.Receive(data, data.Length, 0);
                        ChangeField(data[0]);
                    }
                    else
                        Socket.Send(new byte[] { (byte)Side, 1 });
                });
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

        public async void MakeTurn(int position)
        {
            GameResult result = ChangeField(position);

            if (AgainstAI)
            {

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

        public async void NewGameStarted()
        {
            if (Socket!=null && AgainstAI && AIFirst)
            {
                try
                {
                    byte[] data = new byte[256];
                    await Task.Run(() =>
                    {
                        int bytes = Socket.Receive(data, data.Length, 0);
                    });
                    ChangeField(data[0]);
                }
                catch (Exception ex)
                {
                    ErrorOccured?.Invoke(ex.Message);
                    return;
                }
            }
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
                for (int i = 0; i < Side * Side; i++)
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

            for (int i = 0; i < Side; i++)//по вертикали
            {
                a = true;
                wonPos.Clear();
                sign = Field[i];
                if (sign == CellState.Empty)
                    continue;
                for (int j = 0; j < Side; j++)
                {
                    wonPos.Add(i + j * Side);
                    if (Field[i + j * Side] != sign)
                    {
                        a = false;
                        break;
                    }
                }
                if (a == true)
                    return (GameResult.Won, wonPos.ToArray());
            }
            for (int i = 0; i < Side; i++)//по горизонтали
            {
                a = true;
                wonPos.Clear();
                sign = Field[i * Side];
                if (sign == CellState.Empty)
                    continue;
                for (int j = 0; j < Side; j++)
                {
                    wonPos.Add(i * Side + j);
                    if (Field[i * Side + j] != sign)
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
                for (int i = 0; i < Side * Side; i += Side + 1)
                {
                    wonPos.Add(i);
                    if (Field[i] != sign)
                    {
                        a = false;
                        break;
                    }
                }
                if (a == true)
                    return (GameResult.Won, wonPos.ToArray());
            }
            a = true;
            sign = Field[Side - 1];
            wonPos.Clear();
            if (sign != CellState.Empty)
            {
                for (int i = Side - 1; i < Side * Side - 1; i += Side - 1)
                {
                    wonPos.Add(i);
                    if (Field[i] != sign)
                    {
                        a = false;
                        break;
                    }
                }
                if (a == true)
                    return (GameResult.Won, wonPos.ToArray());
            }

            foreach (CellState item in Field)
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
