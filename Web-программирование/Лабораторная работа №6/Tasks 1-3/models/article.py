from pydantic import BaseModel
from datetime import datetime
import time


class Article(BaseModel):
    id: str
    updated: datetime
    published: datetime
    title: str
    summary: str
    authors: list[str] = []

    @staticmethod
    def from_parsed_entry(entry):
        return Article(
            id=entry["id"],
            updated=datetime.fromtimestamp(time.mktime(entry["updated_parsed"])),
            published=datetime.fromtimestamp(time.mktime(entry["published_parsed"])),
            title=entry["title"],
            summary=entry["summary"],
            authors=list(map(lambda x: x["name"], entry["authors"])),
        )
