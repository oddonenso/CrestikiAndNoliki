namespace TransportLib
{
    public interface ISend
    {
        void Send(CommandType type); 
        void Send(string message);   
    }
}
