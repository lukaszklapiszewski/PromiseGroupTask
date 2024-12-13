namespace ApiClient.Controller.Interface
{
    /// <summary>
    /// Interface for controller classes
    /// </summary>
    internal interface IPaginatedController
    {
        int CurrentPage {  get; set; }
    }
}
