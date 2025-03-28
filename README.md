# csharp_task_queue_processor

📦 Projeto: TaskQueueProcessor
Objetivo: Criar um sistema de processamento assíncrono de tarefas usando filas, onde tarefas são adicionadas por uma API e processadas por um worker em background.

🧱 Arquitetura
ASP.NET Core Web API (produção de mensagens)

Worker Service (consumidor das mensagens)

RabbitMQ ou Azure Service Bus (fila)

Entity Framework Core (persistência de tarefas e status)

Redis (opcional para cache de status ou deduplicação)

🔄 Fluxo resumido
A API recebe uma requisição para processar uma nova tarefa.

A tarefa é salva no banco com status “Pendente” e enviada para a fila.

O worker escuta a fila, processa a tarefa, atualiza o status para “Concluído” ou “Erro”.

A API pode consultar o status da tarefa (com cache se quiser usar Redis).

🧪 Funcionalidades para explorar
Retry automático no worker (com políticas do Polly)

Dead Letter Queue (mensagens que falham após várias tentativas)

Delay entre tentativas

Escalabilidade horizontal do worker

Observabilidade: logs estruturados e métricas (ex: OpenTelemetry + Prometheus)

Mensagens com prioridade

Middleware de validação e rastreamento (ex: correlation ID)

🚀 Dicas para enriquecer o aprendizado
Comece com RabbitMQ local usando Docker.

Depois implemente um provider para Azure Service Bus (ou AWS SQS).

Adicione testes de integração entre API, fila e banco.

Simule falhas no worker para estudar resiliência.

