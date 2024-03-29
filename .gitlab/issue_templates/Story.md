# Описание Issue

Будучи пользователем `<тип пользователя>`

Чтобы `<мотивация желания>`

Я хочу сделать `<действие>`

чтобы получить `<результат>`

## Служебные поля

*  Важность: <SP_1, SP_2, SP_3>
*  Предварительная оценка: <1, 2, 3>
*  Связанные задачи <ссылки>
*  Документы wiki <ссылки>

## Как продемонстрировать

<Сценарий демо или автотест>

# Критерии приемки

<значемые детали реализации истории>

## Памятка по формулировке названия задачи

Краткое описание истории. Оно должно быть однозначным, чтобы разработчик и владелец проекта могли понять о чём идёт речь и отличить одну историю от другой

## Дополнительные замечания

01. PO описывает  общую идею и свое видение проекта на Wiki
02. BA + PO формирует детальное описание проекта на Wiki
03. QA + BA описывают "acceptance criteria"
04. PM + BA

    * формируют "user stories" на основе сформированных "acceptance criteria"
    * Создают "tasks" на доске (Gitlab Issues) и помечают маркером (label) **Story**
    * Устанавливают приоритет, отмечая маркером "PR_1" (наивысший), "PR_2", "PR_3"

05. Dev + PM оценивают "user stories" в "SP" и помечают маркером (label) "SP_1", "SP_2",...
06. Dev + PM набирают "user stories" на текущий спринт исходя из оценки по времени (метки "SP_1", "SP_2",...)
07. Dev + PM создают задачи (tasks) на основе "user stories"
08. Dev + PM

    * оценивают задачи (tasks) в SP и помечают маркером (label) "SP_1", "SP_2",...
    * Устанавливают приоритет, отмечая маркером "PR_1" (наивысший), "PR_2", "PR_3"

09. Dev берет задачу на реализацию, перенося ее из колонки "ToDo" в колонку "Doing"
10. Dev в "Git" репозитории создает отдельную ветку, с именем по следующему шаблону: "#task_[bug/features]_[short description]"
11. Dev после выполнения фиксирует свои изменения в "Git" (делает commit), с именем по следующему шаблону: "#task: [bug/features]: [short description]"
12. Dev "вливает" изменения в своей ветке (свою фичу или исправление бага) в ветку "dev"
13. Dev сообщает в чате QA о выполнении такой-то задачи (если задача подразумевает тестирование)
14. QA берут свою задачу (ассоциированную с выполненной в предыдущем пункте задачей) и перемещают по колонкам ("ToDo" -> "Doing", etc)
