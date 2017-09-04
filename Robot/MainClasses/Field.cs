using System.Windows.Media;

namespace Robot
{
    public class Field 
    {
        public Field(int fieldWidth, int fieldHeight)
        {
            _fieldWidth = fieldWidth;
            _fieldHeight = fieldHeight;
        }

        #region Свойства
        private int _fieldWidth;
        private int _fieldHeight;
        public Color[,] CellsColorArray { get; set; }
      
        #endregion

        #region Методы
        /// <summary>
        /// Создание и заполнение массива ячеек
        /// </summary>
        /// <param name="algorithm"></param>
        public void InitializeField(Color borderColor, Color defaultColor)
        { 
            CellsColorArray = new Color[_fieldWidth, _fieldHeight];

            for (var x = 0; x < _fieldWidth; x++)
                for (var y = 0; y < _fieldHeight; y++)
                    CellsColorArray[x, y] = CheckCellPosition(x, y, borderColor, defaultColor);
        }

        /// <summary>
        /// Проверяет, лежит ли ячейка на границе
        /// Если ячейка лежит на границе, возвращает 2.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Color CheckCellPosition(int x, int y, Color borderColor, Color defaultColor)
        {
            return (x == 0 || y == 0 || x == _fieldWidth - 2 || y == _fieldHeight - 2) ? borderColor : defaultColor;
        }
        #endregion
    }
}
