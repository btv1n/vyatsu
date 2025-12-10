package Commands;

import java.io.File;

import Wd.WorkingDirectory;

import org.slf4j.*;

// Класс реализует удаление директории
// Пример использования:
// Текущая директория -> rmdir my_folder
// Директория C:\Users\Home\Downloads\ -> rmdir /Users/Home/Downloads/new_folder

public class RemoveDirectoryCommand implements Command {
    // Логирование
    private static final Logger log = LoggerFactory.getLogger(RemoveDirectoryCommand.class);

    // описание класса
    private static final String description = "\tУдалить каталог рекурсивно \n\t[path] может быть абсолютным или относительным";
    
    @Override
    public CommandResult execute(String[] args) {
        log.debug("Выполнение RemoveDirectoryCommand.");
        // получает текущий рабочий каталог
        WorkingDirectory wd = WorkingDirectory.getInstance();

        // передано 2 аргумента
        if (args.length == 2) {
            // извлекает путь к удаляемой директории
            String dirPath = args[1];
            File f = null;
            // определяет является путь относительным или абсолютным
            if (dirPath.startsWith("/")) // абсолютный
                f = new File(dirPath);
            else
                f = new File(wd.getPath().concat("/" + dirPath)); // относительный

            // проверяет существование указаного пути
            if (!f.exists()) {
                log.error("RemoveDirectoryCommand - каталог не существует.");
                return new CommandResult(CommandStatus.ERROR, "Каталог не существует");
            }
            // ошибка: путь ведет к файлу, а не к каталогу
            if (!f.isDirectory()) {
                log.error("RemoveDirectoryCommand - не каталог.");
                return new CommandResult(CommandStatus.ERROR, "Не каталог");
            }

            // выполнение рекурсивного удаления
            try {
                delete(f);
            } catch (SecurityException e) {
                // нет прав на удаление директории
                log.error("RemoveDirectoryCommand - невозможно удалить каталог.");
                return new CommandResult(CommandStatus.ERROR, "Невозможно удалить каталог");
            }

            // успешно
            log.debug("RemoveDirectoryCommand выполнена успешно.");
            return new CommandResult(CommandStatus.SUCCESS);
        }

        // передан один аргумент
        if (args.length == 1) {
            log.error("RemoveDirectoryCommand - путь не указан.");
            return new CommandResult(CommandStatus.ERROR, "Путь не указан");
        }

        // передано > 2 аргументов
        log.error("RemoveDirectoryCommand - слишком много параметров.");
        return new CommandResult(CommandStatus.ERROR, "Слишком много параметров");
    }

    // рекурсиивный метод для удаления директории и всего ее содержимого
    private void delete(File dir) {
        log.info("Выполнение delete.");
        // получает список всех файлов и поддиректорий в текущей директории
        for (File fileEntry : dir.listFiles()) {
            // является ли текущий эллемент поддиректорией
            if (fileEntry.isDirectory()) {
                // рекурсивно вызывает delete для удаления вложенной директории
                delete(fileEntry);
            } else {
                // если элемент является файлом, удаляем его
                fileEntry.delete();
            }
        }

        // после удаление всего содержимого, удаляет саму директорию
        dir.delete();
        log.info("delete выполнен успешно.");
    }

    // описание для help
    @Override
    public String getDescription() {
        return description;
    }
}
