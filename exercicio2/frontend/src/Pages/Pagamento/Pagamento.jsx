import BlocosPagamento from "../../Components/BlocosPagamento";
import "./Pagamento.css";

import { useState, useEffect } from "react";
import { useParams, Navigate } from "react-router-dom";

const Pagamento = () => {
  const { id } = useParams();
  const [pagamentos, setPagamentos] = useState([]);
  const [usuario, setUsuario] = useState("");
  const [loading, setLoading] = useState(true);
  const [tipos, setTipos] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetch(`http://localhost:5248/usuario/${id}`)
      .then((res) => {
        return res.json();
      })
      .then((data) => {
        setUsuario(data);
      })
      .catch((err) => {
        console.log(err);
      });

    fetch(`http://localhost:5248/pagamento/${id}`)
      .then((res) => res.json())
      .then((data) => {
        setPagamentos(data);
        setLoading(false);
      })
      .catch((err) => {
        console.log(err);
        setLoading(false);
        setError(true);
      });
  }, [id]);

  // Fiz isso pois separei os grids de pagamentos por Tipo de pagamento
  useEffect(() => {
    const tiposDiferentes = [];
    pagamentos.map((pag) => {
      if (!tiposDiferentes.includes(pag.tipo)) {
        tiposDiferentes.push(pag.tipo);
      }
    });

    setTipos(tiposDiferentes);
  }, [pagamentos]);

  if (loading) {
    return <p>Carregando...</p>;
  }

  // Se der algum erro na busca na webapi retorno para a página de usuários
  if (error) {
    return <Navigate to="/" />;
  }

  return (
    <div id="pagamento">
      <h3>Pagamentos {usuario.nome}</h3>

      <div className="pagamento-container">
        {/* Aqui faço a separação por tipos de pagamento */}
        {tipos.map((tipo) => (
          <BlocosPagamento
            key={tipo}
            pagamentos={pagamentos.filter((pag) => pag.tipo == tipo)}
          />
        ))}
      </div>
    </div>
  );
};

export default Pagamento;
