package Commands;

import java.io.File;

import Wd.WorkingDirectory;

import org.slf4j.*;

// Класс для создания каталогов
// Пример использования:
// Создание в текущей директории: mkdir new_folder
// Создание в C:\Users\Home\Downloads\ -> mkdir /Users/Home/Downloads/new_folder

public class MakeDirectoryCommand implements Command {
    // Логирование
    private static final Logger log = LoggerFactory.getLogger(MakeDirectoryCommand.class);

    // описание класса
    private static final String description = "\tСоздает каталог\n\t[path] может быть абсолютным или относительным";
    
    @Override
    public CommandResult execute(String[] args) {
        log.debug("Выполнение MakeDirectoryCommand.");
        // получает текущий рабочий каталог
        WorkingDirectory wd = WorkingDirectory.getInstance();

        // проверяет, что передан только один аргумент
        if (args.length == 2) {
            // извлекает путь к создаваемой директории
            String dirPath = args[1];
            File f = null;

            // определяет: путь абсолютный или относительный
            if (dirPath.startsWith("/"))
                f = new File(dirPath); // абсолютный
            else
                f = new File(wd.getPath().concat("/" + dirPath)); // относительный

            // проверяем не существует ли уже директория с таким же именем
            if (f.exists()) {
                log.warn("MakeDirectoryCommand - файл или каталог уже существует.");
                return new CommandResult(CommandStatus.ERROR, "Файл или каталог уже существует");
            }

            // создание директории
            try {
                // вызывает метод для создания директории
                if (!f.mkdir()) {
                    log.error("MakeDirectoryCommand - каталог не был создан.");
                    return new CommandResult(CommandStatus.ERROR, "Каталог не был создан");
                }
            } catch (SecurityException e) {
                // ошибка: нет прав для создания директории
                log.error("MakeDirectoryCommand - невозможно создать каталог.");
                return new CommandResult(CommandStatus.ERROR, "Невозможно создать каталог");
            }
            return new CommandResult(CommandStatus.SUCCESS);
        }

        // не указан путь
        if (args.length == 1) {
            log.error("MakeDirectoryCommand - путь не указан.");
            return new CommandResult(CommandStatus.ERROR, "Путь не указан");
        }

        // передано > 2 параметров
        log.error("MakeDirectoryCommand - слишком много параметров.");
        return new CommandResult(CommandStatus.ERROR, "Слишком много параметров");
    }

    // описание для команды help
    @Override
    public String getDescription() {
        return description;
    }
}
