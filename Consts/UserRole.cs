namespace CompanyPortal.Consts
{
    public class UserRole
    {
        public const string Company = "Company";
        public const string Customer = "Customer";
        public const string Admin = "Admin";

        public static readonly string[] AllRoles = { Company, Customer, Admin };

        public static bool IsValidRole(string type)
        {
            return AllRoles.Contains(type);
        }
    }
}
