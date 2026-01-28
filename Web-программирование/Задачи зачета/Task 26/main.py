'''
Задание: Напишите веб-сервис с API функцией, принимающей параметры name и age,
которая возвращает строковое значение "name", вам разрешен вход на сайт" - в случае, если age > 20 
и строковое значение "К сожалению, name, вы еще малы для входа на сайт", если age < 20.
'''

# Установка fastapi:         pip install fastapi
#                            pip install fastapi[standard]
# Запуск:                    fastapi dev main.py
# HTTP запрос с параметрами: http://127.0.0.1:8000/check?name=Семён&age=25
#                            http://127.0.0.1:8000/check?name=Елизавета&age=17

from fastapi import FastAPI

app = FastAPI()

@app.get("/")
async def check_access(name: str, age: int):
    if age > 20:
        return f"{name}, вам разрешен вход на сайт"
    else:
        return f"К сожалению, {name}, вы еще малы для входа на сайт"