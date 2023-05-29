import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import { ValidationPipe } from "@nestjs/common";
import { DocumentBuilder, SwaggerModule } from "@nestjs/swagger";

async function bootstrap() {

  const app = await NestFactory.create(AppModule);

  const config = new DocumentBuilder()
    .setTitle('MouseAPI')
    .setDescription('MouseAPI application')
    .setVersion('0.0.1')
    .addBearerAuth()
    .build();

  app.useGlobalPipes(new ValidationPipe());

  app.enableCors({
    origin: [
      "http://localhost:3000"
    ],
    allowedHeaders: [
      'Accept',
      'Content-Type',
      'Authorization',
    ],
    preflightContinue: false,
    optionsSuccessStatus: 204,
    credentials: true
  });

  const document = SwaggerModule.createDocument(app, config);
  SwaggerModule.setup('api/swagger', app, document);

  await app.listen(8000);
}

bootstrap();
