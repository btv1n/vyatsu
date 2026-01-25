package core;

import core.habr.HabrParser;
import core.habr.HabrSettings;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.IOException;
import java.util.ArrayList;

/**
 * Created by class on 14.10.2021.
 */

class GUI extends JFrame implements ParserWorker.OnCompleted, ParserWorker.OnNewDataHandler<ArrayList<String>> {

    JTextArea textArea;
    ParserWorker<ArrayList<String>> parser;

    public GUI() {
        parser = new ParserWorker<>(new HabrParser());
        parser.onCompletedList.add(this);
        parser.onNewDataList.add(this);
        JFrame.setDefaultLookAndFeelDecorated(true);
        setTitle("Habr");
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

        JPanel rightPanel = new JPanel();
        GridLayout layout = new GridLayout(0, 1, 5, 12);
        rightPanel.setLayout(layout);

        JTextField textStart = new JTextField(10);
        JTextField textEnd = new JTextField(10);
        JButton startButton = new JButton("Start");
        JButton abortButton = new JButton("Abort");

        rightPanel.add(new JLabel("Первая страница"));
        rightPanel.add(textStart);
        rightPanel.add(new JLabel("Последняя страница"));
        rightPanel.add(textEnd);
        rightPanel.add(startButton);
        startButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                int start = Integer.parseInt(textStart.getText());
                int end = Integer.parseInt(textEnd.getText());
                parser.setParserSettings(new HabrSettings(start, end));
                try {
                    parser.Start();
                } catch (IOException ex) {
                    ex.printStackTrace();
                }
            }
        });
        rightPanel.add(abortButton);

        add(rightPanel, BorderLayout.EAST);
        JPanel panel = new JPanel();
        textArea = new JTextArea(20, 30);
        textArea.setLineWrap(true);
        textArea.setWrapStyleWord(true);
        JScrollPane jsp = new JScrollPane(textArea);
        jsp.setVerticalScrollBarPolicy(ScrollPaneConstants.VERTICAL_SCROLLBAR_ALWAYS);
        panel.setLayout(new FlowLayout());
        panel.add(jsp);

        add(panel, BorderLayout.CENTER);

        pack();
        setVisible(true);
    }

    @Override
    public void OnNewData(Object sender, ArrayList<String> args) {
        for (String s : args) {
            textArea.append(s + "\n");
        }
    }

    @Override
    public void OnCompleted(Object sender) {
        JOptionPane.showMessageDialog(this,
                "Загрузка закончена");
    }

}

public class Form {

    public static void main(String[] args) {
        javax.swing.SwingUtilities.invokeLater(new Runnable() {
            public void run() {
                JFrame frame = new GUI();
            }
        });
    }
}
