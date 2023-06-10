import { Module } from '@nestjs/common';
import { UserModule } from "./modules/user/user.module";
import { ConfigModule } from "@nestjs/config";
import { DatabaseModule } from "./database/database.module";
import { AuthModule } from "./modules/auth/auth.module";
import { MediaService } from "./modules/media/media.service";
import { MapModule } from "./modules/map/map.module";

@Module({
  imports: [
    DatabaseModule,
    ConfigModule.forRoot({
      envFilePath: `.env.development`,
    }),
    UserModule,
    MediaService,
    AuthModule,
    MapModule
  ],
  controllers: [],
  providers: [],
})
export class AppModule {}
