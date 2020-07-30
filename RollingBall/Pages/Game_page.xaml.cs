using RollingBall.Models.Levels;

namespace RollingBall.Pages
{
    /// <summary>
    /// Inheritance base game class
    /// xaml present View part of MVVM pattern
    /// </summary>
    public partial class Game_page : Base_game_page
    {
        public Game_page(Level level) : base(level)
        {
            InitializeComponent();
        }
    }
}
