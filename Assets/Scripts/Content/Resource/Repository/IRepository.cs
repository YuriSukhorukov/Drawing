using System;
using System.Collections.Generic;

namespace Content.Resource.Repository
{
    public interface IRepository<T> where T:IHaveID
    {
        
        /// <summary>
        /// Инициализирует репозиторий 
        /// </summary>
        void Init();

        /// <summary>
        /// Обновляет репозиторий
        /// </summary>
        void Update();
        
        /// <summary>
        /// Добавляет в репозиторий новый элемент
        /// </summary>
        /// <param name="item"></param>
        void Add(T item);
        
        /// <summary>
        /// Возвращает элемент с заданным id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(uint id);
        
        /// <summary>
        /// Возвращает список элементов с указанными id. Отсутствующие в репозитории id игнорируются
        /// </summary>
        /// <param name="ids">Список id возвращаемых элементов</param>
        /// <returns></returns>
        List<T> Get(List<uint> ids);

        /// <summary>
        /// Возвращает первый найденный в репозитории элемент, удовлетворяющий условию поиска
        /// </summary>
        /// <param name="match">Условие поиска</param>
        /// <returns></returns>
        T Find(Predicate<T> match);

        /// <summary>
        /// Возвращает список всех элементов репозитория, удовлетворяющих условию поиска
        /// </summary>
        /// <param name="match">Условие поиска</param>
        /// <returns></returns>
        List<T> FindAll(Predicate<T> match);

    }
}