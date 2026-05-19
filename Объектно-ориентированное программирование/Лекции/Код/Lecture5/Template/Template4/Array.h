#pragma once
//#include <cstddef>
#include <new>

template <class T>
class Array
{
private:
    size_t size;
    T* array;

public:
    Array(size_t size, const T& value = T());
    Array();
    Array(const Array& obj);
    Array& operator=(const Array& obj);
    size_t getSize() const;

    T& operator[](size_t i);
    const T& operator[](size_t i) const;
    ~Array();
    void mem_free();
};

template<class T>
inline Array<T>::Array(size_t size, const T& value)
{
    this->size = size;
    // конструктора по умолчанию не существует, поэтому память выделяем через char(количество * Размер T) и 
    //приводим к нужному нам типу
    array = (T*) new char[size * sizeof(T)];
    // проходим по массиву и явно вызываем конструктор с параметром
    for (size_t i = 0; i != size; ++i)
        new (array + i) T(value);
    //У типа T есть конструктор с параметром     
    //Память уже выделена выше, поэтому используем placement new operator с конструктором с параметром
    //placement new оператор не выделяет память, а получает своим аргументом адрес на уже выделенную каким-либо образом память. 
    //Размещение (инициализация) объекта происходит путём вызова конструктора, и объект создается в памяти по указанному адресу.
    //Часто такой метод применяют, когда у класса нет конструктора по умолчанию и при этом нужно создать массив объектов.
    //В placement new можно передать предварительно выделенную память и построить объект в переданной памяти. 
    //При использовании placement new в круглых скобках указывают адрес, а после этого пишут тип. 
}

template<class T>
inline Array<T>::Array()
{
    size = 0;
    array = nullptr;
}

template<class T>
inline Array<T>::Array(const Array& obj)
{
    size = obj.size;
    array = (T*) new char[size * sizeof(T)];
    for (size_t i = 0; i != size; ++i)
        new (array + i) T(obj[i]);
}

template<class T>
inline size_t Array<T>::getSize() const
{
    return size;
}

template<class T>
inline Array<T>::~Array()
{
    //нельзя выполнить delete [] array, т.к. память выделяли через char
    mem_free();
}

template<class T>
inline void Array<T>::mem_free()
{
    for (size_t i = 0; i != size; ++i)
        array[i].~T(); // Явный вызов деструктора для каждого элемента
    delete[](char*) array; // Выделяли память через [] char! Удаляем через [] char!
}

template <class T>
inline Array<T>& Array<T>::operator=(Array const& obj)
{
    if (this != &obj)
    {
        mem_free();
        size = obj.size;
        array = (T*) new char[size * sizeof(T)];
        for (size_t i = 0; i != size; ++i)
            new (array + i) T(obj[i]);
    }
    return *this;
}

template <class T>
inline T& Array<T>::operator[](const size_t i)
{
    return *(array +i);
}

template <class T>
inline const T& Array<T>::operator[](const size_t i) const
{
    return *(array + i);
}
