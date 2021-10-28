namespace TwentyTwoSeven.Common.Enums
{
    public static class AppEnum
    {
        public const int TRANSFERSUCCESS = 1;

        public const int TRANSFERFAILED= 2;
    }

    public static class AccountStatus
    {
        public const int Inactive = 0;

        public const int Active = 1;

        public const int Overdrawn = 2;
    }

    public static class AccountType
    {
        public const int CHEQUE = 1;

        public const int SAVINGS = 2;

        public const int CREDIT = 3;
    }
}
