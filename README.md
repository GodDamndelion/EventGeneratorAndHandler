# EventGeneratorAndHandler

Для работы проекта понадобится Docker.

Для запуска проекта нужно прописать в PowerShell студии команды docker-compose build и docker-compose up.

Либо придётся запускать БД с PostgreSQL на порту 25432 с настройками из appsettings.json, одну студию с запускаемым проектом EventHandler и другую студию с запускаемым проектом EventGenerator именно в таком порядке.
