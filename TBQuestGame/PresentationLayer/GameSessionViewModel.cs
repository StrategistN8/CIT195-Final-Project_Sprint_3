using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestGame.Models;

namespace TBQuestGame.PresentationLayer
{
    public class GameSessionViewModel
    {


        #region ENUMS



        #endregion

        #region FIELDS

        private Player _player;
        private List<string> _messages;
        private DateTime _gameStartTime;

        #endregion

        #region PROPERTIES

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public string MessageDisplay
        {
            get { return FormatMessagesForViewer(); }
        }

        #endregion

        #region CONSTRUCTORS
     
        /// <summary>
        /// Default Constructor
        /// </summary>
        public GameSessionViewModel()
        {

        }

        /// <summary>
        /// (Temp) Constructor that requires player data.
        /// </summary>
        /// <param name="player"></param>
        public GameSessionViewModel(Player player)
        {
            _player = player;
            InitializeView();
        }

        /// <summary>
        /// Constructor that requires player data and initial messages.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="initialMessages"></param>
        public GameSessionViewModel(Player player, List<string> initialMessages)
        {
            _player = player;
            _messages = initialMessages;
            InitializeView();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// initial setup of the game session view
        /// </summary>
        private void InitializeView()
        {
            _gameStartTime = DateTime.Now;
        }

        /// <summary>
        /// (Velis) Generates a sting of mission messages with time stamp with most current first
        /// </summary>
        /// <returns>string of formated mission messages</returns>
        private string FormatMessagesForViewer()
        {
            List<string> lifoMessages = new List<string>();

            for (int index = 0; index < _messages.Count; index++)
            {
                lifoMessages.Add($" <T:{GameTime().ToString(@"hh\:mm\:ss")}> " + _messages[index]);
            }

            lifoMessages.Reverse();

            return string.Join("\n\n", lifoMessages);
        }

        /// <summary>
        /// (Velis) running time of game
        /// </summary>
        /// <returns></returns>
        private TimeSpan GameTime()
        {
            return DateTime.Now - _gameStartTime;
        }

        /// <summary>
        /// Exit Application:
        /// </summary>
        public void QuitApplication()
        {

            Environment.Exit(0);
        }
        #endregion

        #region EVENTS



        #endregion
    }
}
