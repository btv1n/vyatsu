package Commands;

import java.util.Map;

import org.slf4j.*;

// Класс выводит справку по всем возможным командам

public class HelpCommand implements Command {
    // Логирование
    private static final Logger log = LoggerFactory.getLogger(HelpCommand.class);

    // описание класса
    private static final String description = "\tВыводит список команд";

    // все возможные команды ; map - ключ, значение
    private final Map<String, Command> cmds;

    // конструктор сохраняет переданную Map команд в поле класса
    public HelpCommand(Map<String, Command> cmds) { this.cmds = cmds; }


    @Override
    public CommandResult execute(String[] args) {
        log.debug("Выполнение HelpCommand.");
        // проходит по всем командам, выводит их и описание
        for (Map.Entry<String, Command> entry : cmds.entrySet()) {
            System.out.println(entry.getKey() + "\n" + entry.getValue().getDescription());
        }
        log.info("HelpCommand выполнена успешно.");
        return new CommandResult(CommandStatus.SUCCESS, null);
    }

    // возвращает описание самой команды help
    @Override
    public String getDescription() {
        return description;
    }
}
