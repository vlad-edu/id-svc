namespace IdService.Core.Enums
{
    public enum UserStatus : byte
    {
        /// <summary>
        /// Awaiting administrator confirmation.
        /// </summary>
        Pending = 0,

        /// <summary>
        /// TThis is the normal status of a user.
        /// </summary>
        Active = 1,

        /// <summary>
        /// The account owner or a user with sufficient permissions can set another user as suspended so that the user can no longer access resources. This is a temporary alternative to deleting a user.
        /// </summary>
        Suspended = 2,

        /// <summary>
        /// Setting a user to Deleted blocks the user from logging in, and removes them from the list of users. Once they have been deleted there is no way to restore their ability to log in.
        /// </summary>
        Deleted = 3,
    }
}
