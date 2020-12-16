using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ScrollerEngineData;

namespace ScrollerEngineGameEditor
{
    public class EditorGameObjects : IEnumerable
    {
        private GameEntry _gameEntry;

        public EditorGameObjects(GameEntry gameEntry)
        {
            _gameEntry = gameEntry;
        }

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            foreach (var item in _gameEntry.Levels)
                yield return item;

            foreach (var item in _gameEntry.AvailableCharacters)
                yield return item;

            foreach (var item in _gameEntry.AvailableEnemies)
                yield return item;

            yield return new GoalPoint();

            yield return new StartPoint();
        }

        #endregion
    }
}
