using UnityEngine;

namespace Geometry
{

	public static class Planimetry
	{

		public static bool IsPointInPolygon (ref Vector2 point, Vector2[] polygon)
		{

            int max = polygon.Length;
            int v1 = max - 1;
            bool res = false;

            for (int v2 = 0; v2 < max; v1 = v2++) {
                // Проверка на четность: нечетное число пересечений сторон полигона произвольным лучом
                // означает, что начало луча лежит внутри полигона
                res = (res != IsUpwardRayIntersects(ref point, ref polygon[v1], ref polygon[v2]));
            }
            return res;
		}

		// Пересекает ли направленный из точки origin строго вверх луч отрезок v1-v2 
		public static bool IsUpwardRayIntersects(ref Vector2 origin, ref Vector2 v1, ref Vector2 v2)
		{
            // Мега-портянка дублирует идущий ниже код, но на больших данных дает выигрыш в производительности
            // из-за отсечения трудоемких вычислений на явно тупиковых вариантах
            if (v1.x < v2.x)
            {
                if (origin.x < v1.x || origin.x > v2.x)
                    return false;
            }
            else if (v1.x > v2.x)
            {
				if (origin.x > v1.x || origin.x < v2.x)
					return false;
            }
            else
            {
                return false;
            }
			if (v1.y < v2.y)
			{
				if (origin.y > v2.y)
					return false;
			}
			else if (v1.y > v2.y)
			{
				if (origin.y > v1.y)
					return false;
			}
            // Универсальная проверка пересечений для любых вариантов
            Vector2 edge1 = v1 - origin;
			Vector2 edge2 = v2 - origin;
			// Counterclockwise angle sign:
			float sign1 = Mathf.Sign(Vector3.Cross(Vector2.up, edge1).z);
			float sign2 = Mathf.Sign(Vector3.Cross(Vector2.up, edge2).z);
			float angle1 = (edge1 != Vector2.zero) ? Vector2.Angle(Vector2.up, edge1) : 0f;
			float angle2 = (edge2 != Vector2.zero) ? Vector2.Angle(Vector2.up, edge2) : 0f;

			return !Mathf.Approximately(sign1, sign2) ? 
				angle1 + angle2 <= 180 :
				(
					Mathf.Approximately(angle1, 0) && 
					!Mathf.Approximately(angle1, angle2) && 
					Mathf.Approximately(edge1.x, origin.x) // Костыль против ошибок округления при сравнении малых углов
				);
		}

		public static bool IsLine (Vector2 a, Vector2 b, Vector2 c)
		{
			float area = a.x * ( b.y - c.y ) + b.x * ( c.y - a.y ) + c.x * ( a.y - b.y );
			return Mathf.Approximately(area, 0f);
		}

	}

}