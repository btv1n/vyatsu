#include <iostream>
#include <string>
#include <sstream>
#include <fstream>

template <typename T>
struct Node2
{
    T data;
    Node2<T> *next;
    Node2<T> *prev;

    inline explicit Node2(T data = 0) : data(data), next(nullptr), prev(nullptr){};

    inline ~Node2()
    {
        delete next;
    }

    /// @brief Adds Node to the end of list
    void add(T data);

    /// @brief Initialises Node and inserts it after this
    /// @return Added element
    Node2<T> *add_after(T data = 0);

    /// @brief Initialises Node pointing to this
    /// @return Added element
    Node2<T> *add_before(T data);

    static void delete_first(Node2<T> *&list);

    size_t count() const;
    Node2<T> *last();
};

template <typename T>
inline void Node2<T>::add(T data)
{
    Node2<T> *l = this->last();
    l->next = new Node2(data);
    l->prev = l;
}

template <typename T>
inline Node2<T> *Node2<T>::add_after(T data)
{
    Node2<T> *t = new Node2(data);
    if (this->next != nullptr)
        this->next->prev = t;
    t->next = this->next;
    t->prev = this;

    this->next = t;
    return t;
}

template <typename T>
inline Node2<T> *Node2<T>::add_before(T data)
{
    Node2<T> *t = new Node2<T>(data);

    if(this->prev != nullptr)
        this->prev->next = t;
    t->prev = this->prev;
    this->prev = t;
    t->next = this;
    return t;
}

template <typename T>
inline void Node2<T>::delete_first(Node2<T> *&list)
{
    Node2<T> *t = list;
    list = list->next;

    t->next = nullptr;
    delete t;
}

template <typename T>
size_t Node2<T>::count() const
{
    size_t count = 0;

    for (const Node2 *p = this; p != nullptr; p = p->next)
        count++;

    return count;
}

template <typename T>
Node2<T> *Node2<T>::last()
{
    Node2<T> *p = this;

    while (p->next != nullptr)
        p = p->next;

    return p;
}