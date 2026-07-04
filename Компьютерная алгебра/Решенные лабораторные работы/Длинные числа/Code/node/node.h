#include <iostream>
#include <string>
#include <sstream>
#include <fstream>

template <typename T>
struct Node
{
    T data;
    Node<T> *next;

    inline explicit Node(T data = 0) : data(data), next(nullptr){};

    inline ~Node()
    {
        delete next;
    }

    /// @brief Adds Node to the end of list
    void add(T data);

    /// @brief Initialises Node and inserts it after this
    /// @return Added element
    Node<T> *add_after(T data);

    /// @brief Initialises Node pointing to this
    /// @return Added element
    Node<T> *add_before(T data);

    /// @brief Inserts Node between given
    /// @param first Node pointing to last
    /// @return Inserted Node
    static Node<T> *insert_between(Node *first, Node *last, T data);

    static void delete_first(Node<T> *&list);

    size_t count() const;
    Node<T> *last();
    T max() const;

    Node<T> *find_first(T needle);

    static Node<T> *from_fstream(std::ifstream &is);
    static Node<T> *from_string(std::string &s);
};

template <typename T>
std::ostream &operator<<(std::ostream &os, const Node<T> *list)
{
    if (list == nullptr)
        return os << "{}";
    os << "{";
    const Node *p = list;
    while (p->next != nullptr)
    {
        os << p->data << ", ";
        p = p->next;
    }
    os << p->data << "}";

    return os;
}

template <typename T>
inline void Node<T>::add(T data)
{
    Node<T> *l = this->last();
    l->next = new Node<T>(data);
}

template <typename T>
inline Node<T> *Node<T>::add_after(T data)
{
    Node<T> *t = new Node(data);
    t->next = this->next;
    this->next = t;
    return t;
}

template <typename T>
inline Node<T> *Node<T>::add_before(T data)
{
    Node<T> *t = new Node<T>(data);
    t->next = this;
    return t;
}

template <typename T>
inline Node<T> *Node<T>::insert_between(Node *first, Node *last, T data)
{
    if (first->next != last)
        throw std::exception("First does not point to last");

    Node<T> *t = new Node<T>(data);
    t->next = last;
    first->next = t;
    return t;
}

template <typename T>
inline void Node<T>::delete_first(Node<T> *&list)
{
    Node<T> *t = list;
    list = list->next;
    
    t->next = nullptr;
    delete t;
}

template <typename T>
size_t Node<T>::count() const
{
    size_t count = 0;

    for (const Node *p = this; p != nullptr; p = p->next)
        count++;

    return count;
}

template <typename T>
Node<T> *Node<T>::last()
{
    Node<T> *p = this;

    while (p->next != nullptr)
        p = p->next;

    return p;
}

template <typename T>
Node<T> *Node<T>::from_fstream(std::ifstream &ifs)
{
    if (ifs.eof() || ifs.peek() == std::ifstream::traits_type::eof())
        return nullptr;
    Node<T> *node = new Node<T>();
    ifs >> node->data;
    node->next = Node<T>::from_fstream(ifs);
    return node;
}

template <typename T>
Node<T> *Node<T>::from_string(std::string &s)
{
    if (s.size() == 0)
        return nullptr;
    std::stringstream ss(s);

    Node<T> *first = nullptr, *p = nullptr;
    while (!ss.eof())
    {
        int data;
        ss >> data;
        Node<T> *new_node = new Node<T>(data);
        if (first == nullptr)
        {
            first = new_node;
            p = first;
        }
        else
        {
            p->next = new_node;
            p = p->next;
        }
    }

    return first;
}

template <typename T>
T Node<T>::max() const
{
    T m = this->data;

    for (const Node<T> *i = this; i != nullptr; i = i->next)
        if (i->data > m)
            m = i->data;

    return m;
}

template <typename T>
inline Node<T> *Node<T>::find_first(T needle)
{
    for (Node<T> *i = this; i != nullptr; i = i->next)
    {
        if (i->data == needle)
            return i;
    }
    return nullptr;
}