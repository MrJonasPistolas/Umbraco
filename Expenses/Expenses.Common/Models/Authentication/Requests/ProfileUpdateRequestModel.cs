namespace Expenses.Common.Models.Authentication.Requests
{
    public class ProfileUpdateRequestModel
    {
        public string Fullname { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }

        /// <summary>
        ///     The path to redirect to when update is successful, if not specified then the user will be
        ///     redirected to the current Umbraco page
        /// </summary>
        public string? RedirectUrl { get; set; }
    }
}
