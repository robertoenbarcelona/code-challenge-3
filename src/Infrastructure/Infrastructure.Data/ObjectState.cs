
namespace Infrastructure.Data
{
    /// <summary>
    /// Entity states
    /// </summary>
    public enum ObjectState
    {
        /// <summary>
        /// When entity is loaded into a repository or restored
        /// </summary>
        Unchanged,
        /// <summary>
        /// When an entity is attached to a repository and signed for addition
        /// </summary>
        Added,
        /// <summary>
        /// When an entity is attached to a repository
        /// </summary>
        Modified,
        /// <summary>
        /// When an entity is attached to a repository and signed for deletion
        /// </summary>
        Deleted
    }
}
