Название проекта
DayBook ежедневник

Описание проекта
Веб апи проект с круд операциями, authorization/authentification

Технологии и инструменты

.NET 8
PostgreSQL,
RabbitMQ,
Docker (для RabbitMQ),
Swagger,
JWT аутентификация и авторизация,
Паттерн Unit of Work,
Принципы SOLID,
Чистая архитектура,
Интерсептеры,
Миграции базы данных,
Репозиторий паттерн

Структура проекта:

- /DayBook:
  - /Core: 
    - /DayBook.Application
    - /DayBook.Domain
  - /Infrastucture:
    - /DayBook.Consumer
    - /DayBook.DAL
    - /DayBook.Producer
  - /Presentation:
    - /DayBook.Api  

Установка

Клонируйте репозиторий: git clone https://github.com/your/repository.git
Установите зависимости
Настройте базу данных PostgreSQL и RabbitMQ.
Примените миграции: dotnet ef database update
Запустите проект: dotnet run
