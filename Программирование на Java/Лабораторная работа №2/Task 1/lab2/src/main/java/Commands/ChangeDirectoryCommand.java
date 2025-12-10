package Commands;

import java.io.File;

import Wd.WorkingDirectory;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;


// Класс реализует команду для смены рабочего каталога
// Пример использования:
// Текущий каталог: C:\Users\Alex
// cd Documents                   - переход в дочерний каталог
// cd ..                          - переход к родительскому каталогу
// cd ../Downloads                - относительный путь
// cd home/user/projects          - абсолютный путь

public class ChangeDirectoryCommand implements Command {
    // Логирование
    private static final Logger log = LoggerFactory.getLogger(ChangeDirectoryCommand.class);

    // описание команды
    private static final String description = "\tИзменяет каталог\n\t[path] может быть абсолютным или относительным";

    @Override
    public CommandResult execute(String[] args) {
        log.debug("Выполнение ChangeDirectoryCommand.");
        // получает единственный экземпляр рабочего каталога
        WorkingDirectory wd = WorkingDirectory.getInstance();

        // cd + path
        if (args.length == 2) {
            String path = args[1]; // путь введенный пользователем
            File f = null;         // объект File, соответствующий этому пути

            if (path.startsWith("/"))
                f = new File(path); // абсолютный путь
            else
                // создаём путь, объединяя текущий путь и относительный
                f = new File(wd.getPath().concat("/" + path));

            // проверяет существование пути и является ли он директорией
            // try
            // C:\Users\Alex\Projects -> cd ..\Documents -> C:\Users\Alex\Documents
            // getCanonicalPath() - убирает лишнее . или ..
            if (!f.isDirectory()) {
                log.error("Ошибка ChangeDirectoryCommand - не директория.");
                return new CommandResult(CommandStatus.ERROR, "Не директория.");
            }
            try {
                wd.setPath(f.getCanonicalPath());
            } catch (Exception e) {
                log.error("Ошибка ChangeDirectoryCommand - {}.", e.getMessage());
                return new CommandResult(CommandStatus.ERROR, e.getMessage());
            }

            // успешное выполнение операции
            log.debug("Успешное выполнение ChangeDirectoryCommand.");
            return new CommandResult(CommandStatus.SUCCESS);
        }

        // cd
        // пользователь не указал путь
        if (args.length == 1)
        {
            log.error("Ошибка ChangeDirectoryCommand - путь не указан.");
            return new CommandResult(CommandStatus.ERROR, "Путь не указан");
        }
        log.error("Ошибка ChangeDirectoryCommand - слишком много параметров.");
        return new CommandResult(CommandStatus.ERROR, "Слишком много параметров");
    }

    // описание класса для команды help
    @Override
    public String getDescription() {
        return description;
    }
}
