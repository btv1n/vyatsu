package parser;

public interface OnNewDataHandler<T> {
    void onNewData(Object sender, T e);
}
