package Wd;

// использование шаблона проектирования Singleton - запрещает создание объекта с помощью конструторов
public class WorkingDirectory {
    // приватное поле для хранения единственного экземпляра класса
    private static WorkingDirectory directory;
    private String path;

    // приватный конструктор - запрещает создавание объектов с помощью new вне класса
    private WorkingDirectory(String path) {
        this.path = path;
    }

    // проверяет существует ли уже экзепляр directory; если нет создает его один раз, иначе возвращает тот же самый объект
    public static WorkingDirectory getInstance() {
        return WorkingDirectory.getInstance(null);
    }

    public static WorkingDirectory getInstance(String path) {
        if (directory == null) {
            directory = new WorkingDirectory(path == null ? System.getProperty("user.dir") : path);
        }
        return directory;
    }

    public String getPath() {
        return path;
    }

    public void setPath(String newPath) {
        path = newPath;
    }
}
