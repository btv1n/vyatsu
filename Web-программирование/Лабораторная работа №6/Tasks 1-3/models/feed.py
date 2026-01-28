from pydantic import BaseModel
import feedparser
from .article import Article
import time
from datetime import datetime


class Feed(BaseModel):
    title: str
    id: str
    total_results: int
    start_index: int
    items_per_page: int
    updated: datetime
    entries: list[Article] = []

    @staticmethod
    def from_xml(text: str):
        parsed = feedparser.parse(text)

        feed = parsed["feed"]
        parsed_entries = parsed["entries"]

        entries = []
        for entry in parsed_entries:
            entries.append(Article.from_parsed_entry(entry))
        return Feed(
            title=feed["title"],
            id=feed["id"],
            total_results=int(feed["opensearch_totalresults"]),
            start_index=int(feed["opensearch_startindex"]),
            items_per_page=int(feed["opensearch_itemsperpage"]),
            updated=datetime.fromtimestamp(time.mktime(feed["updated_parsed"])),
            entries=entries,
        )
