namespace Content.Resource.Repository.DataProviders
{
    public interface IDataProvider
    {
        /// <summary>
        /// URL в вебе или на локальной файловой сисетме, содержащий сериализованные данные
        /// </summary>
        string Source { get; }

        /// <summary>
        /// Обновление сериализованного представления набора данных из привязанного Source
        /// </summary>
        /// <returns>Сериализованные данные</returns>
        string Read();

        /// <summary>
        /// Отправка сериализованного набора данных в указанный Source
        /// </summary>
        /// <param name="data"></param>
        void Write(string data);
    }
}