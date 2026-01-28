from fastapi import FastAPI
from fastapi.responses import JSONResponse
from fastapi.templating import Jinja2Templates
from fastapi.middleware.cors import CORSMiddleware
import uvicorn
from translate import Translator
import ArxivService
import asyncio


APP_HOST = "localhost"
APP_PORT = 8085

app = FastAPI()
templates = Jinja2Templates(directory="templates/")
_translator = Translator(to_lang="ru")

origins = ["*"]
app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)


@app.get("/feed")
async def feed(query: str, start: int = 0, max_results: int = 10):
    result = ArxivService.searchByTitle(query, start, max_results)
    return result


@app.get("/byId")
async def feed(ids: str):
    result = ArxivService.searchById(ids.split(","))
    return result


@app.get("/translate")
async def test(id: str):
    result = ArxivService.searchById([id])
    if len(result.entries) == 0:
        return JSONResponse({}, 404)
    result = result.entries[0]

    result.title = _translator.translate(result.title)

    if len(result.summary) > 500:

        async def translate(s):
            return _translator.translate(s)

        original = result.summary
        result.summary = ""
        while len(original) > 500:
            i = original.rfind(".", 0, 500)
            result.summary += _translator.translate(original[: i + 1])

            original = original[i + 1 :]

        if len(original) > 0:
            result.summary += _translator.translate(original)
    else:
        result.summary = _translator.translate(result.summary)
    return result


if __name__ == "__main__":
    uvicorn.run("app:app", host=APP_HOST, port=APP_PORT, workers=1, reload=True)
