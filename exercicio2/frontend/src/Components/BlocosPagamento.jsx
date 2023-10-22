import { useState } from "react";
import "./BlocosPagamento.css";

const BlocosPagamento = ({ pagamentos }) => {
  const [modal, setModal] = useState(false);

  // Criei esses 2 para ficar mais fácil de excluir e utilizar o modal, mas tem outras maneiras melhores
  const [bill, setBill] = useState(""); // Esse é para o modal
  const [bills, setBills] = useState(pagamentos); // Esse é para a página em geral

  const handleClick = (id) => {
    setModal(true);
    // Coloco o pagamento selecionado em bill para mostrar no modal
    setBill(bills.find((pag) => pag.id == id));
  };

  const closeModal = () => {
    setModal(false);
  };

  const deletarPagamento = (id) => {
    // Deleto o pagamento e fecho o modal
    setBills(bills.filter((bill) => bill.id !== id));
    setModal(false);
  };

  return (
    <div className="grid-container">
      <table>
        <thead>
          <tr>
            <th>Id do Pagamento</th>
            <th>Tipo do Pagamento</th>
            <th>Valor</th>
          </tr>
        </thead>
        <tbody>
          {bills &&
            bills.map((pagamento) => {
              return (
                <tr
                  key={pagamento.id}
                  onClick={() => handleClick(pagamento.id)}
                >
                  <td>{pagamento.id}</td>
                  <td>{pagamento.tipo}</td>
                  <td>R${pagamento.valor}</td>
                </tr>
              );
            })}
        </tbody>
      </table>

      {modal && (
        <div className="background">
          <div className="modal">
            <p>
              <strong>DESEJA EXCLUIR?</strong>
            </p>
            <p>
              Id: {bill.id}, Tipo: {bill.tipo}, Valor: R${bill.valor}
            </p>
            <div>
              <button
                onClick={() => deletarPagamento(bill.id)}
                className="excluir"
              >
                Excluir
              </button>
              <button onClick={closeModal} className="fechar">
                Fechar
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default BlocosPagamento;
