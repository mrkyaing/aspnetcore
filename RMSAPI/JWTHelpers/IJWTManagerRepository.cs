using RMSAPI.Models;

namespace RMSAPI.JWTHelpers {
    public interface IJWTManagerRepository {
        TokenModel Authenticate(UserModel user);
    }
}
