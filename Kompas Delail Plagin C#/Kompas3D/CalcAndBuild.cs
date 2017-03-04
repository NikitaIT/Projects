using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Kompas3D
{
    using WindowsFormsApplication1;
    using Error;
    using Kompas3D;
    using Kompas6API5;
    using Kompas6Constants;

    /// <summary>
    /// Класс расчёта и построений
    /// </summary>
    public class CalcAndBuild
    {
        private double mainRad;
        private double stupRad;
        private double mountHoleRad;
        private double radHolePos;
        private double depth;
        private double holeDiam;
        private KompasObject _kompas;

        public CalcAndBuild() {}
        /// <summary>
        ///Проверка, расчёт параметров и создание детали 
        /// </summary>
        /// <param name="depth">Толщина диска</param>
        /// <param name="mainDiam">Диаметр диска</param>
        /// <param name="mainDiam2">Диаметр выступа</param>
        /// <param name="diam">Диаметр посадочного отверстия</param>
        /// <param name="holeDiam">Диаметр отверстий под крепёж</param>
        /// <param name="_kompas">Пустой чертеж детали</param>
        /// <param name="message">Обработчик ошибок</param>
        public CalcAndBuild(ushort depth, ushort mainDiam, ushort mainDiam2, ushort diam, ushort holeDiam,
            KompasObject _kompas, StatusMessage message)
        {
            this._kompas = _kompas;
            uint massCode = Calc(depth, mainDiam, mainDiam2, diam, holeDiam);
            message.ErrorMessage(massCode);
            if (massCode == 0)
            {
                Build();
            }
        }
        /// <summary>
        /// Валидация переменных
        /// </summary>
        /// <param name="depth">Толщина диска</param>
        /// <param name="mainDiam">Диаметр диска</param>
        /// <param name="mainDiam2">Диаметр выступа</param>
        /// <param name="diam">Диаметр посадочного отверстия</param>
        /// <param name="holeDiam">Диаметр отверстий под крепёж</param>
        /// <returns>
        /// Возвращает код ошибки, 0 если нет ошибок
        /// </returns>
        public uint Calc(ushort depth, ushort mainDiam, ushort mainDiam2, ushort diam, ushort holeDiam)
        {
            this.depth = depth;
            this.holeDiam = holeDiam;
            mainRad = mainDiam / 2;
             stupRad = mainDiam2 / 2;
             mountHoleRad = diam / 2;
             radHolePos = (stupRad - mountHoleRad) / 2 + mountHoleRad;

            #region Проверки

            //Проверяем толщину диска, если меньше 1 мм - ошибка
            if (depth < 0.9 | depth > 40)
            {
                return (uint)StatusMessage.Status.NotValidDiskThickness;
            }

            //Проверяем диаметр диска, если меньше 50 мм - ошибка
            if (mainRad < (50 / 2) | mainRad > 400 / 2)
            {
                return (uint)StatusMessage.Status.NotValidDiscDiameter;
            }

            //Проверяем диаметр выступа, если меньше 80% от диаметра диска - ошибка
            if (stupRad >= mainRad * 0.8)
            {
                return (uint)StatusMessage.Status.NotValidProtrusionDiameter;
            }

            //Проверяем диаметр посадочного отверстия, если меньше 80% - ошибка
            if (mountHoleRad >= stupRad * 0.8)
            {
                return (uint)StatusMessage.Status.NotValidPlantDiameter;
            }

            //Проверяем диаметры отверстий под крепёж, - диаметры не должны "перерезать" материал
            if (holeDiam >= stupRad - mountHoleRad - holeDiam / 2)
            {
                return (uint)StatusMessage.Status.BigFixtureDiameter;
            }

            //Проверяем диаметры отверстий под крепёж, если меньше М2 - ошибка
            if (holeDiam < 2)
            {
                return (uint)StatusMessage.Status.SmallFixtureDiameter;
            }

            #endregion

            return (uint)StatusMessage.Status.NotFound;
        }

        /// <summary>
        /// Построение детали
        /// </summary>
        private void Build()
        { 

            //  Операции выдавливания диска
            
            //выдавливаем диск
            Kompas det6 = new Kompas(_kompas);
            det6.CreateCircle(0, 0, mainRad, depth, "XOY");
            //выдавливаем ступицу
            Kompas det5 = new Kompas(_kompas);
            det5.CreateCircle(0, 0, stupRad, depth * 2, "XOY");
            //вырезаем поднутрение
            Kompas det4 = new Kompas(_kompas);
            det4.CreateCircleHole(0, 0, stupRad - depth / 2, depth, "XOY", "Blind");
            //вырезаем посадочное отверстие
            Kompas det3 = new Kompas(_kompas);
            det3.CreateCircleHole(0, 0, mountHoleRad, depth * 2, "XOY", "ThroughAll");
            //вырезаем крепежные отверстия
            //1
            Kompas det2_1 = new Kompas(_kompas);
            det2_1.CreateCircleHole(0, radHolePos, holeDiam / 2, depth * 2, "XOY", "ThroughAll");
            //2
            Kompas det2_2 = new Kompas(_kompas);
            det2_2.CreateCircleHole(radHolePos, 0, holeDiam / 2, depth * 2, "XOY", "ThroughAll");
            //3
            Kompas det2_3 = new Kompas(_kompas);
            det2_3.CreateCircleHole(0, 0 - radHolePos, holeDiam / 2, depth * 2, "XOY", "ThroughAll");
            //4
            Kompas det2_4 = new Kompas(_kompas);
            det2_4.CreateCircleHole(0 - radHolePos, 0, holeDiam / 2, depth * 2, "XOY", "ThroughAll");
        }

    }
}
