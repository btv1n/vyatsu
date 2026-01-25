package ru.oschepkov;


public interface IConverter {
    public void convert(String pathSource, String pathTarget) throws Exception;
    public void convert(String pathSource) throws Exception;
}
