const API_BASE = import.meta.env.VITE_API_URL ?? "/api";

async function request(url, options = {}) {
  const response = await fetch(url, {
    headers: {
      "Content-Type": "application/json",
      ...options.headers
    },
    ...options
  });

  if (response.status === 204) {
    return null;
  }

  if (!response.ok) {
    const message = await response.text();
    throw new Error(message || `Erro HTTP ${response.status}`);
  }

  const contentType = response.headers.get("content-type") ?? "";
  if (contentType.includes("application/json")) {
    return response.json();
  }

  return null;
}

/**
 * Cliente HTTP espelhando IBaseRepository do backend.
 * @param {string} resource Nome do recurso (ex.: "produtos", "categorias")
 */
export function createRepositoryClient(resource) {
  const base = `${API_BASE}/${resource}`;

  return {
    getByIdAsync(id, cancellationToken) {
      return request(`${base}/${id}`, { signal: cancellationToken });
    },

    getAllAsync(cancellationToken) {
      return request(base, { signal: cancellationToken });
    },

    addAsync(entity, cancellationToken) {
      return request(base, {
        method: "POST",
        body: JSON.stringify(entity),
        signal: cancellationToken
      });
    },

    updateAsync(id, entity, cancellationToken) {
      return request(`${base}/${id}`, {
        method: "PUT",
        body: JSON.stringify(entity),
        signal: cancellationToken
      });
    },

    deleteAsync(id, cancellationToken) {
      return request(`${base}/${id}`, {
        method: "DELETE",
        signal: cancellationToken
      });
    },

    existsAsync(id, cancellationToken) {
      return request(`${base}/${id}/exists`, { signal: cancellationToken });
    }
  };
}
