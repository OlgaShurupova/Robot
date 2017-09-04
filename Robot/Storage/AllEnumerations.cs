using System.ComponentModel;

namespace Robot
{
    public enum AllActions
    {
        [Description("Движение")]
        Move,

        [Description("Поворот")]
        Turn,

        [Description("Заполнение")]
        Filling,

        [Description("Изучение")]
        Study
    };

    public enum Directions
    {
        [Description("Направо")]
        Right,

        [Description("Налево")]
        Left
    };

    public enum AllColors
    {
        [Description("Цвет ячеек 1")]
        White,

        [Description("Цвет ячеек 2")]
        Black,

        [Description("Цвет границы")]
        BorderColor
    };

    /// <summary>
    /// Определяет возможные направления движения робота 
    /// </summary>
    public enum Side
    {
        Right = 90,
        Down = 180,
        Left = 270,
        Up = 0
    };
}
