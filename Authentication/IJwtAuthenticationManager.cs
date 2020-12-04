using USMBAPI.Models;

namespace USMBQuizzAPI.Authentication
{
    public interface IJwtAuthenticationManager
    {
        public string Authenticate(Professor professor);
    
    }
}
