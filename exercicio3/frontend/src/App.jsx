import "./App.css";
import { useState } from "react";

function App() {
  const [arquivosSelecionados, setArquivosSelecionados] = useState([]);
  const [textos, setTextos] = useState([]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const formData = new FormData();

    for (let i = 0; i < arquivosSelecionados.length; i++) {
      formData.append("pdfFiles", arquivosSelecionados[i]);
    }

    try {
      const res = await fetch("http://localhost:5148/api/DadosPdf/upload", {
        method: "POST",
        body: formData,
      });

      if (res.status === 200) {
        const data = await res.json();
        setTextos(data);
      } else {
        console.log("Erro: " + res.status);
      }
    } catch (error) {
      console.log(error);
    }
  };

  console.log(textos);

  return (
    <>
      <form onSubmit={handleSubmit}>
        <label>
          <span>Selecione os arquivos:</span>
          {/* No onChange adiciono os arquivos enviados, é aqui que os arquivos ficam "salvos" */}
          <input
            type="file"
            multiple
            onChange={(e) =>
              setArquivosSelecionados([
                ...arquivosSelecionados,
                ...e.target.files,
              ])
            }
          />
        </label>
        <p>Arquivos selecionados:</p>
        {/* Mostra o nome dos arquivos selecionados */}
        <ul>
          {arquivosSelecionados.map((arquivos, i) => (
            <li key={i}>{arquivos.name}</li>
          ))}
        </ul>

        <input type="submit" value={"Enviar"} />
      </form>

      {textos.length > 0 && (
        <table>
          <thead>
            <tr>
              <th>Nome do Arquivo</th>
              <th>Natureza da Operação</th>
              <th>Valor Total da Nota</th>
              <th>Dados Adicionais</th>
            </tr>
          </thead>
          <tbody>
            {textos.map((texto, i) => (
              <tr key={i}>
                <td>{texto.nomeDoArquivo}</td>
                <td>{texto.naturezaDaOperacao}</td>
                <td>{texto.valorTotalDaNota}</td>
                <td>{texto.dadosAdicionais}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </>
  );
}

export default App;
