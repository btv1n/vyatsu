package CommandTests;

import Commands.MakeDirectoryCommand;
import Commands.CommandResult;
import Commands.CommandStatus;
import Wd.WorkingDirectory;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.io.TempDir;

import java.io.File;
import java.nio.file.Path;

import static org.junit.jupiter.api.Assertions.*;

class MakeDirectoryCommandTest {

    // Аннотация @TempDir создает временную директорию для каждого теста
    @TempDir
    Path tempDir;

    @Test
    void testCreateDirectory() {
        MakeDirectoryCommand command = new MakeDirectoryCommand();
        WorkingDirectory wd = WorkingDirectory.getInstance(tempDir.toString());


        String[] args = {"mkdir", "newfolder"};
        CommandResult result = command.execute(args);

        // проверка выполнения команды
        assertEquals(CommandStatus.SUCCESS, result.status);

        // проверяем, что новая директория создана
        File created = new File(tempDir.toFile(), "newfolder");
        assertTrue(created.exists());
    }

    @Test
    void testCreateExistingDirectory() {
        // создает директорию
        new File(tempDir.toFile(), "existing").mkdir();

        MakeDirectoryCommand command = new MakeDirectoryCommand();
        WorkingDirectory wd = WorkingDirectory.getInstance(tempDir.toString());

        // попытка создать директорию, которая уже создана
        String[] args = {"mkdir", "existing"};
        CommandResult result = command.execute(args);

        assertEquals(CommandStatus.ERROR, result.status);
    }

    @Test
    void testNoArguments() {
        MakeDirectoryCommand command = new MakeDirectoryCommand();

        // недостаточное количество аргументов
        String[] args = {"mkdir"};
        CommandResult result = command.execute(args);

        assertEquals(CommandStatus.ERROR, result.status);
    }
}