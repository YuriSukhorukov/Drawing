/*
 *	The MIT License (MIT)
 *
 *	Copyright (c) 2014 Mateusz Majchrzak
 *
 *	Permission is hereby granted, free of charge, to any person obtaining a copy
 *	of this software and associated documentation files (the "Software"), to deal
 *	in the Software without restriction, including without limitation the rights
 *	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *	copies of the Software, and to permit persons to whom the Software is
 *	furnished to do so, subject to the following conditions:
 *
 *	The above copyright notice and this permission notice shall be included in all
 *	copies or substantial portions of the Software.
 *
 *	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 *	SOFTWARE.
 *
 *  Modified by Pirog
 */

using UnityEngine;

namespace Utils
{
	/// <summary>
	/// Абстрактная обертка для существующих в единственном экземпляре компонентов.
	/// ВАЖНО: обращаться к экземпляру только через свойство Instance! 
	/// </summary>
	public abstract class AbstractMonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T instance;

	    // ReSharper disable once StaticMemberInGenericType
	    // Disabled due to intended behaviour
		private static bool isDestroyed;

		/// <summary>
		/// Статический геттер для единственного экземпляра синглтона 
		/// </summary>
		public static T Instance
		{
		    get
		    {
		        if (isDestroyed)
		            return null;

		        if (instance == null)
		        {
		            instance = FindObjectOfType(typeof(T)) as T;

		            if (instance == null)
		            {
		                GameObject gameObject = new GameObject(typeof(T).Name);
		                DontDestroyOnLoad(gameObject);

		                instance = gameObject.AddComponent(typeof(T)) as T;
		            }
		        }

		        return instance;
		    }
		}

		/// <summary>
		/// Принудительное удаление экземпляра единственного инстанса (последующее создание нового невозможно!)
		/// </summary>
		public virtual void OnDestroy()
		{
		    if (instance)
		        Destroy(instance);

		    instance = null;
		    isDestroyed = true;
		}
	}
}