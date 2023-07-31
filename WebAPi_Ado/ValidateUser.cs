namespace WebAPi_Ado
{
    public class ValidateUser
    {
        public static bool Login (string username, string password)
        {
            if (username=="Admin" && password=="Pass") 
            { 
                return true;
            }
            else 
            { 
                return false;
            }
        }
    }
}
