from requests import get
from models.feed import Feed

_BASE_URL = "http://export.arxiv.org/api"


def searchByTitle(query: str, start: int = 0, max_results=10):
    assert start >= 0
    assert max_results > 0
    res = get(
        _BASE_URL + "/query",
        params={
            "search_query": f'ti:"{query}"',
            "start": start,
            "max_results": max_results,
        },
    )

    return Feed.from_xml(res.text)


def searchById(ids: list[str]) -> Feed:
    res = get(
        _BASE_URL + "/query",
        params={
            "id_list": ",".join(ids),
        },
    )

    return Feed.from_xml(res.text)
