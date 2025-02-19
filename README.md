1. Создаешь базу с именем ProjectManagementBD далее удаляешь папку миграции из обозревателя решений
2. Открываешь консоль диспетчера пакетов и выполняешь по очереди
2.1. Add-Migration InitialCreate
2.2. Update-Database
3. далее открываешь терминал и пишешь по очереди
3.1. Dir
3.2. cd ProjectManagementApi
3.3. dotnet run seeddata
