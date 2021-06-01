using System;
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

        public GameModel(CellState firstTurn, bool againstAI)
        {
            CurrentTurn = firstTurn;
            Field = new CellState[9];
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
                for (int i = 0; i < 9; i++)
                    Field[i] = CellState.Empty;
                GameEnd?.Invoke(result, wonPos);

            }
            return result;
        }

        private (GameResult result, int[] wonPos) CheckIfGameEnd()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Field[i] != CellState.Empty && Field[i] == Field[i + 3] && Field[i + 3] == Field[i + 6])
                    return (GameResult.Won, new int[] { i, i + 3, i + 6 });
            }
            for (int i = 0; i < 8; i += 3)
            {
                if (Field[i] != CellState.Empty && Field[i] == Field[i + 1] && Field[i + 1] == Field[i + 2])
                    return (GameResult.Won, new int[] { i, i + 1, i + 2 });
            }
            if (Field[0] != CellState.Empty && Field[0] == Field[4] && Field[4] == Field[8])
                return (GameResult.Won, new int[] { 0, 4, 8 });
            if (Field[2] != CellState.Empty && Field[2] == Field[4] && Field[4] == Field[6])
                return (GameResult.Won, new int[] { 2, 4, 6 });

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
