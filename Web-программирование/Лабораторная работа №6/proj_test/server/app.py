from fastapi import FastAPI, Request, Body
from fastapi.responses import JSONResponse
from fastapi.templating import Jinja2Templates
from fastapi.middleware.cors import CORSMiddleware
import uvicorn
import json
from typing import Any
from translate import Translator

APP_HOST = "localhost"
APP_PORT = 8085

app = FastAPI()
templates = Jinja2Templates(directory="templates/")

origins = ["*"]
app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

_translator = Translator(to_lang="ru")

@app.post("/test")
async def test(query: str = Body()):
    query_json = json.loads(query)
    origin_text = query_json["query_text"]
    translated_text = _translator.translate(origin_text)
    return JSONResponse(
        {
            "status": "GOOD POST",
            "query_text": origin_text,
            "translated_text": translated_text,
        }
    )


@app.get("/test")
async def test():
    return JSONResponse({"message": "GOOD GET"})


@app.get("/")
async def welcome(request: Request) -> dict:
    return templates.TemplateResponse(
        "home.html", {"request": request, "welcome_string": "Hello, friend!"}
    )


if __name__ == "__main__":
    uvicorn.run("app:app", host=APP_HOST, port=APP_PORT, workers=1, reload=True)
