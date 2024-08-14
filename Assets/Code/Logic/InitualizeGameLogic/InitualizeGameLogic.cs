using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
namespace Logic.InitializeGameLogic
{
    public static class InitializeGameLogic
    {
        public static async Task InitializeGameLogicAsync()
        {
            await Task.Delay(10000);
            Task initializeBreadCrumbsTask = InitializeGameLogic.InitializeBreadCrumsAsync();
            Task sortBreadCrumbsToPathsTask = InitializeGameLogic.SortBreadCrumsToPathsAsync();

        }
        public static async Task InitializeBreadCrumsAsync()
        {
            await Task.Delay(10000);
        }
        public static async Task SortBreadCrumsToPathsAsync()
        {
            await Task.Delay(10000);
        }
    }  
    public class Game
    {
        public async Task StartGameAsync()
        {
            Task initializeGameLogicAsync = InitializeGameLogic.InitializeGameLogicAsync();
            

            await Task.WhenAll(initializeGameLogicAsync);

        }
    }
}