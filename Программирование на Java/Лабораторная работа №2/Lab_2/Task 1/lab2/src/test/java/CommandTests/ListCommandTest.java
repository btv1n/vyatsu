package CommandTests;

import Commands.ListCommand;
import Commands.CommandResult;
import Commands.CommandStatus;
import Wd.WorkingDirectory;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.io.TempDir;

import java.io.File;
import java.nio.file.Path;

import static org.junit.jupiter.api.Assertions.*;

class ListCommandTest {

    // Аннотация @TempDir создает временную директорию для каждого теста
    @TempDir
    Path tempDir;

    @Test
    void testListCurrentDirectory() {
        ListCommand command = new ListCommand();
        WorkingDirectory wd = WorkingDirectory.getInstance(tempDir.toString());

        String[] args = {"ls"};
        CommandResult result = command.execute(args);

        assertEquals(CommandStatus.SUCCESS, result.status);
    }

    @Test
    void testListWithTree() throws Exception {
        // создание поддиректории во временной директории
        new File(tempDir.toFile(), "subdir").mkdir();

        ListCommand command = new ListCommand();
        WorkingDirectory wd = WorkingDirectory.getInstance(tempDir.toString());


        String[] args = {"ls", "-t"};
        CommandResult result = command.execute(args);

        assertEquals(CommandStatus.SUCCESS, result.status);
    }

    @Test
    void testListWithUnknownArgument() {
        ListCommand command = new ListCommand();

        // передача неверного параметра
        String[] args = {"ls", "-invalid"};
        CommandResult result = command.execute(args);

        assertEquals(CommandStatus.ERROR, result.status);
    }
}