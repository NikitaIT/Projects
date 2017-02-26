using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    using Kompas6API5;
    using Kompas6Constants3D;
    using Error;

    class Kompas
    {
        /// <summary>
        /// Вспомогательная, для отладки функция
        /// </summary>
        /// <param name="a"></param>
        public void Message(string a)
        {
            MessageBox.Show(a);
        }

        public KompasObject kompas;

  //      public ksDocument3D doc;

        public Kompas (KompasObject kompas)
        {
            this.kompas = kompas;
        }

        /// <summary>
        /// Выдавливание окружности с вырезом
        /// </summary>
        /// <param name="x1">координата Х центра окружности</param>
        /// <param name="y1">координата У центра окружности</param>
        /// <param name="rad">радиус окружности</param>
        /// <param name="depth">Глубина окружности</param>
        /// <param name="plane">На какой плоскости</param>
        /// <param name="defType"></param>
        /// <param name="chamfer"> </param>
        public void CreateCircleHole (double x1, double y1, double rad, double depth, string plane, string defType)
        {

            var doc = (ksDocument3D)kompas.ActiveDocument3D();
            var part = (ksPart)doc.GetPart ( (short)Part_Type.pTop_Part );
            if (part != null)
            {
                // Создаем новый эскиз
                ksEntity entitySketch = (ksEntity)part.NewEntity ( (short)Obj3dType.o3d_sketch );
                if (entitySketch != null)
                {
                    // интерфейс свойств эскиза
                    ksSketchDefinition sketchDef = (ksSketchDefinition)entitySketch.GetDefinition ( );
                    if (sketchDef != null)
                    {
                        // получим интерфейс базовой плоскости
                        ksEntity basePlane;
                        if (plane == "XOY")
                        {
                            basePlane = (ksEntity)part.GetDefaultEntity ( (short)Obj3dType.o3d_planeXOY );
                        }
                        else
                        {
                            basePlane = (ksEntity)part.GetDefaultEntity ( (short)Obj3dType.o3d_planeXOZ );
                        }
                        sketchDef.SetPlane ( basePlane );	// установим плоскость базовой для эскиза
                        entitySketch.Create ( );			// создадим эскиз1

                        // интерфейс редактора эскиза
                        ksDocument2D sketchEdit = (ksDocument2D)sketchDef.BeginEdit ( );

                        //круглое отверстие
                        sketchEdit.ksCircle ( x1, y1, rad, 1 );
                        sketchDef.EndEdit ( );	// завершение редактирования эскиза
                        // вырежим выдавливанием

                        ksEntity entityCutExtr = (ksEntity)part.NewEntity ( (short)Obj3dType.o3d_cutExtrusion );

                        if (entityCutExtr != null)
                        {
                            ksCutExtrusionDefinition cutExtrDef = (ksCutExtrusionDefinition)entityCutExtr.GetDefinition ( );

                            if (cutExtrDef != null)
                            {
                                cutExtrDef.cut = true;
                                cutExtrDef.directionType = (short)Direction_Type.dtReverse;

                                if (defType == "ThroughAll")
                                {
                                    cutExtrDef.SetSideParam ( false, (short)End_Type.etThroughAll, depth );
                                }
                                else if (defType == "Blind")
                                {
                                    cutExtrDef.SetSideParam ( false, (short)End_Type.etBlind, depth );
                                }

                                cutExtrDef.SetSketch ( entitySketch );
                                entityCutExtr.Create ( );	// создадим операцию вырезание выдавливанием
                                   // CreateChamfer ( "XOZ" );
                                sketchDef.EndEdit ( ); // завершение редактирования эскиза
                            }
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Создание окружности с выдавливанием
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="rad"></param>
        /// <param name="depth"></param>
        /// <param name="plane"></param>
        public void CreateCircle (double x1, double y1, double rad, double depth, string plane)
        {
           // ksPart part = (ksPart)doc.GetPart((short)Part_Type.pTop_Part);	// новый компонент
            var doc = (ksDocument3D)kompas.ActiveDocument3D();            
            if (doc == null || doc.reference == 0)
            {
                doc = (ksDocument3D)kompas.Document3D();
                doc.Create(true, true);

                doc.author = "Ethereal";
                doc.comment = "3D Steps - Step3d1";
                doc.UpdateDocumentParam();
            }
            var part = (ksPart)doc.GetPart ( (short)Part_Type.pTop_Part );
            if (part != null)
            {
                // Создаем новый эскиз
                ksEntity entitySketch = (ksEntity)part.NewEntity ( (short)Obj3dType.o3d_sketch );
                if (entitySketch != null)
                {
                    // интерфейс свойств эскиза
                    ksSketchDefinition sketchDef = (ksSketchDefinition)entitySketch.GetDefinition ( );
                    if (sketchDef != null)
                    {
                        // получим интерфейс базовой плоскости
                        ksEntity basePlane;
                        if (plane == "XOY")
                        {
                            basePlane = (ksEntity)part.GetDefaultEntity ( (short)Obj3dType.o3d_planeXOY );
                        }
                        else
                        {
                            basePlane = (ksEntity)part.GetDefaultEntity ( (short)Obj3dType.o3d_planeXOZ );
                        }
                        sketchDef.SetPlane ( basePlane );	// установим плоскость базовой для эскиза
                        sketchDef.angle = 45;           // угол поворота эскиза
                        entitySketch.Create ( );			// создадим эскиз1

                        // интерфейс редактора эскиза
                        ksDocument2D sketchEdit = (ksDocument2D)sketchDef.BeginEdit ( );
                        


                        // отверстие
                        sketchEdit.ksCircle ( x1, y1, rad, 1 );
                        sketchDef.EndEdit ( );	// завершение редактирования эскиза
                        ksEntity entityExtr = (ksEntity)part.NewEntity ( (short)Obj3dType.o3d_baseExtrusion );
                        if (entityExtr != null)
                        {
                            // интерфейс свойств базовой операции выдавливания
                            ksBaseExtrusionDefinition extrusionDef =
                                 (ksBaseExtrusionDefinition)entityExtr.GetDefinition ( );
                            // интерфейс базовой операции выдавливания
                            if (extrusionDef != null)
                            {
                                extrusionDef.directionType = (short)Direction_Type.dtNormal;
                                // направление выдавливания
                                extrusionDef.SetSideParam ( true, // прямое направление
                                                          (short)End_Type.etBlind, // строго на глубину
                                                          depth ); // Расстояние выдавливания
                                extrusionDef.SetSketch ( entitySketch ); // эскиз операции выдавливания

                                entityExtr.Create ( ); // создать операцию
                                sketchDef.EndEdit ( ); // завершение редактирования эскиза
                            }
                        }
                    }
                }
            }
        }
    }
}

