namespace Joygame.Joystore.API.Exceptions
{
    public static class AppExceptions
    {
        public class UserNotFoundException : BaseException
        {
            public UserNotFoundException() { }

            public UserNotFoundException(string message)
                : base(message)
            {
            }
        }
        public class InvalidCredentialException : BaseException
        {
            public InvalidCredentialException() { }

            public InvalidCredentialException(string message)
                : base(message)
            {
            }
        }

        public class InvalidTokenException : BaseException
        {
            public InvalidTokenException() { }

            public InvalidTokenException(string message)
                : base(message)
            {
            }
        }

        public class RecordNotFoundException : BaseException
        {
            public RecordNotFoundException() { }

            public RecordNotFoundException(string message)
                : base(message)
            {
            }
        }
    }
}
