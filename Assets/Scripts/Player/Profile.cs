namespace Player
{
    public class Profile
    {
        public Profile(string id)
        {
            ID = id;
        }

        private static string ID { get; set; }
      
        public static string GetID()
        {
            return ID;
        }
    }
}