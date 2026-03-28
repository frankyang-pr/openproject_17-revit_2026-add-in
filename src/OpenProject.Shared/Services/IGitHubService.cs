using OpenProject.Shared.Models;
using Optional;

namespace OpenProject.Shared.Services
{
    public interface IGitHubService
    {
        Option<Release> GetLatestRelease();
    }
}
