
namespace Soat.HappyNet.Silverlight.Tests.Production.Default1.Interfaces
{
    /// <summary>
    /// Cette interface définit le Vue Model pour le Default1
    /// </summary>
    public interface IDefault1ViewModel
    {
        #region View Property

        /// <summary>
        /// Vue Default1 à utliser 
        /// </summary>
        IDefault1View View { get; }

        #endregion

        string Test { get; set; }
    }
}
